using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;

namespace LSP.Lib
{
    class Evaluator
    {
        MethodInfo objMI;
        object obj;
        public Evaluator(string NameSpace, string ClassName, string Code)
        {
            // 1.CSharpCodePrivoder
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();

            // 2.ICodeComplier
            ICodeCompiler objICodeCompiler = objCSharpCodePrivoder.CreateCompiler();

            // 3.CompilerParameters
            CompilerParameters objCompilerParameters = new CompilerParameters();
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Linq.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Threading.Tasks.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Core.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Data.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Xml.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Xml.Linq.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Deployment.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Net.Http.dll");
            objCompilerParameters.ReferencedAssemblies.Add("LSPFramework.dll");
            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = true;

            // 4.CompilerResults
            CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(objCompilerParameters, Code);

            if (cr.Errors.HasErrors)
            {
                Console.WriteLine("编译错误：");
                foreach (CompilerError err in cr.Errors)
                {
                    Console.WriteLine(err.ErrorText);
                }
            }
            else
            {
                Assembly objAssembly = cr.CompiledAssembly;
                obj = objAssembly.CreateInstance(NameSpace + "." + ClassName);
            }
        }
        public void RunMethhod(string Method)
        {
            obj.GetType().GetMethod(Method).Invoke(obj, null);
        }


    }
}
