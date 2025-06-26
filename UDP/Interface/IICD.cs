using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDP.Interface
{
    public abstract class IICD
    {
        public int Id { set; get; }  // 
        public string Code { set; get; }  // ICD编码
        public string Name { set; get; }  // 名称
        public string Length { set; get; }  // 字节总长度
        public string Cycle { set; get; } //  周期
    }
}
