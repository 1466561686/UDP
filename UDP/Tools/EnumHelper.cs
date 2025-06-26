using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP.Tools
{
    public class EnumHelper
    {

    }
    // ICD类型
    public enum EICD_Type
    {
        接收,
        发送
    }
    // 发送类型
    public enum ESend_Type
    {
        周期,
        事件
    }
    // 校验和方式
    public enum ECheck_Sum_Mode
    {
        方式一,
        方式二,
        方式三
    }
    // 发送类型
    public enum ESymbol
    {
        无,
        有
    }
    // 发送类型
    public enum ESortOrder
    {
        高低,
        低高
    }

    // 设备类型
    public enum EDeviceType
    {
        UDP,
        COM,
        RS422
    }
    // 硬件类型
    public enum EHardType
    {
        UDP,
        COM
    }

    // 接口数据类型
    public enum EInterfaceDataType
    {
        总线类,
        模拟量,
        数字量,
        开关量
    }

    // 菜单等级
    public enum EMenuLevel
    {
        一级菜单,
        二级菜单,
        三级菜单
    }

    // 是否启用
    public enum EIsUse
    {
        不启用,
        启用
    }

    public enum EChannelZB
    {
        主主,
        主备,
        备主,
        备备
    }
    public enum EChannelABCD
    {
        A通道,
        B通道,
        C通道,
        D通道
    }


    public enum EValueType
    {
        物理量,
        源码
    }

    // 数据编码
    public enum EDataCode
    {
        十进制,
        二进制,
        十六进制
    }

    public enum EUDPRecvFlag
    {
        RS422,
        RT
    }
}
