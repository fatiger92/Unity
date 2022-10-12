using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text;
using UnityEngine;

// 마샬링은 게임 시작 전 로딩 데이터 로드에 사용, 런타임에 사용하면 안됨.
// 편하긴 하지만 속도는 느리기 때문.
public class MarshalTableConstant // 고정 버퍼 사이즈를 사용하기 위해 정의.
{
    public const int charBufferSize = 256;
}

// 제네릭 클래스로 만듬 - 박싱 / 언박싱이 일어나지 않도록 하기 위해.
public class TableRecordParser<TMarshalStruct> 
{
    // TMarshalStruct 반환형 메서드 - 테이블에서 line 하나를 읽어서 구조체로 만들어 주는 역할. 
    public TMarshalStruct ParseRecordLine(string line)
    {
        // TMarshalStruct 크기에 맞춰서 Byte 배열 할당
        Type type = typeof(TMarshalStruct);
        
        // System.Runtime.InteropServices.Marshal
        // TMarshalStruct는 사용자 정의 형식인데 사이즈는 몇?
        // 넘어오는 타입의 구조체에 따라서 변함.
        // 구조체 변수의 총 byte의 합이 structSize가 되는 것임.
        int structSize = Marshal.SizeOf(type);        
        //Debug.Log(structSize);
        
        // byte 총합만큼 배열을 만들어 줌.
        byte[] structBytes = new byte[structSize]; 
        int structBytesIndex = 0;

        // line 문자열을 spliter 로 자르기 위해 변수에 담아 놓음.
        const string spliter = ",";
        
        // 위의 spliter로 line 문자열을 잘라서 fieldDataList 배열에 넣음
        string[] fieldDataList = line.Split(spliter.ToCharArray());

        //Type dataType; // 타입을 보고 바이너리에 파싱하여 삽입
        //string splited; // 이게 왜 또 있는지 모르겠음.
        byte[] fieldByte; // fieldByte 저장용
        byte[] keyBytes; // KeyByte의 저장용

        // System.Reflection.FieldInfo
        FieldInfo[] fieldInfos = type.GetFields();
        //Debug.Log($"fieldInfos.Length :: {fieldInfos.Length}");
        
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            Type dataType = fieldInfos[i].FieldType;
            //Debug.Log(fieldInfos[i].Name.ToString());
           
            string splited = fieldDataList[i];
            
            MakeBytesByFieldType(out fieldByte, dataType, splited);

            Buffer.BlockCopy(fieldByte, 0, structBytes, structBytesIndex, fieldByte.Length);
            //Debug.Log($"fieldByte.Length :: {fieldByte.Length}");
            structBytesIndex += fieldByte.Length;

            // 첫번째 필드를 Key 값으로 사용하기 위해 백업
            if (i == 0)
                keyBytes = fieldByte;
        }

        // mashaling
        TMarshalStruct tStruct = MakeStructFromBytes<TMarshalStruct>(structBytes);
        //AddData(keyBytes, tStruct);
        return tStruct;
    }

    /// <summary>
    /// 문자열 splite를 주어진 dataType 에 맞게 fieldByte 배열에 변환해서 반환
    /// </summary>
    /// <param name="fieldByte">결과 값을 받을 배열</param>
    /// <param name="dataType">splite를 변환할때 사용될 자료형</param>
    /// <param name="splite">변환할 값이 있는 문자열</param>
    protected void MakeBytesByFieldType(out byte[] fieldByte, Type dataType, string splite)
    {
        fieldByte = new byte[1];

        if (typeof(int) == dataType)
        {
            fieldByte = BitConverter.GetBytes(int.Parse(splite));    // System.BitConverter
        }
        else if (typeof(float) == dataType)
        {
            fieldByte = BitConverter.GetBytes(float.Parse(splite));
        }
        else if (typeof(bool) == dataType)
        {
            bool value = bool.Parse(splite);
            int temp = value ? 1 : 0;

            fieldByte = BitConverter.GetBytes((int)temp);
        }
        else if (typeof(string) == dataType)
        {
            fieldByte = new byte[MarshalTableConstant.charBufferSize];      // 마샬링을 하기위해서 고정크기 버퍼를 생성
            byte[] byteArr = Encoding.UTF8.GetBytes(splite);                // System.Text.Encoding;
            // 변환된 byte 배열을 고정크기 버퍼에 복사
            Buffer.BlockCopy(byteArr, 0, fieldByte, 0, byteArr.Length);     // System.Buffer;
        }
    }

    /// <summary>
    /// 마샬링을 통한 byte 배열의 T형 구조체 변환
    /// </summary>
    /// <typeparam name="T">마샬링에 적합하게 정의된 구조체의 타입</typeparam>
    /// <param name="bytes">마샬링할 데이터가 저장된 배열</param>
    /// <returns>변환된 T형 구조체</returns>
    public static T MakeStructFromBytes<T>(byte[] bytes) 
    {
        int size = Marshal.SizeOf(typeof(T)); // 총 바이트 수 284
        
        // 마샬 메모리 할당
        // IntPtr는 비트 너비가 포인터와 같은 부호 있는 정수
        // AllocHGlobal()는 지정된 바이트 수를 사용하여 프로세스의 관리되지 않는 메모리(unmanaged memory)에서 메모리를 할당한다.
        IntPtr ptr = Marshal.AllocHGlobal(size);    
        
        // 바이트 배열을 마샬 복사
        Marshal.Copy(bytes, 0, ptr, size);          

        T tStruct = (T)Marshal.PtrToStructure(ptr, typeof(T));  // 메모리로부터 T형 구조체로 변환
        Marshal.FreeHGlobal(ptr);       // 할당된 메모리 해제
        return tStruct; // 변환된 값 반환
    }
}