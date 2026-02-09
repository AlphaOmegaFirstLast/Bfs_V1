using Admin.App;
using Admin.App.CodeWriters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.App
{
    // Track code-changes
    public class CodeTracker
    {
        public List<CodeChangeEntry> CodeChangeList { get; set; } = new List<CodeChangeEntry>();

        public CodeTracker() { 
        }

        public void Start(string? input)
        {
            var generatedCode = input ?? string.Empty; 
            var entry = new CodeChangeEntry();
            entry.GeneratedCode = generatedCode;
            CodeChangeList.Add(entry);
        }

        public CodeChangeEntry CreateEntry(string? input)
        {
            var entry = new CodeChangeEntry();
            entry.GeneratedCode = input ?? string.Empty;
            CodeChangeList.Add(entry);
            return entry;
        }

        public string? GetGeneratedCode()
        {
            // last as it is the end of the pipeline.
            return CodeChangeList.Last().GeneratedCode;
        }

        public List<string> GetSnippetList()
        {
            var list = new List<string>();
            //Todo make sure that there is a flag that force code generation even if code is repeated.
            // After replay the "Modify Logic", track all changes and replace them from the generated code
            var codeChanges = CodeChangeList.Where(x => x.SnippetList.Count > 0).SelectMany(x => x.SnippetList).ToList();
            foreach (var codeChange in codeChanges)
            {
                if (codeChange != null && !string.IsNullOrEmpty(codeChange.Snippet))
                    list.Add(codeChange.Snippet.Trim());
            }

            return list;
        }
    }

    public class CodeChangeEntry
    {
        // accumlated generated code snippets. so far to this point.
        public string GeneratedCode = string.Empty;

        public List<SnippetEntry> SnippetList = new List<SnippetEntry>();

        public string CreateSnippet(string generatedContent)
        {
            var snippetBuilder = new StringBuilder();
            snippetBuilder.AppendLine(generatedContent);
            var snippet = snippetBuilder.ToString();
            return snippet;
        }

        public void UpdateEntry( string generatedCode, string snippet, ICodeWriter writer, PlaceHolderInfo placeHolder, string placeHolderName, int index)
        {
            // whether snippet was found or not found and added to code, record the snippet. so it could be tracked down if Rollback is needed.
            var snippetEntry = new SnippetEntry() { Snippet = snippet, Writer = writer, PlaceHolderInfo = placeHolder, PlaceHolderName = placeHolderName, Index = index };
            SnippetList.Add(snippetEntry);
            GeneratedCode = generatedCode;
        }
    }

    public class SnippetEntry
    {
        public string Snippet = string.Empty;
        public string PlaceHolderName = string.Empty;
        public ICodeWriter Writer = null;
        public PlaceHolderInfo PlaceHolderInfo = null;
        public int Index = 0;
    }
}
