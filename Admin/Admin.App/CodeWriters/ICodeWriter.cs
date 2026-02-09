using Admin.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.App.CodeWriters
{
    public interface ICodeWriter
    {
        public string Name { get; set; }
        string ToContent(CodeBase codeInfo, string input, PlaceHolderInfo? placeHolder);
        string SetRelated(CodeBase codeInfo, string input, PlaceHolderInfo? placeHolder);
    }
}
