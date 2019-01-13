using JavaResolver.Class.Constants;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Provides a high-level representation of an exception handler in a method body of a Java class file.
    /// </summary>
    public class ExceptionHandler
    {
        public ExceptionHandler()
        {
        }
        
        internal ExceptionHandler(JavaClassFile classFile, MethodBody methodBody, ExceptionHandlerInfo info)
        {
            Start = methodBody.Instructions.GetByOffset(info.StartOffset);
            End = methodBody.Instructions.GetByOffset(info.EndOffset);
            HandlerStart = methodBody.Instructions.GetByOffset(info.HandlerOffset);

            var constantInfo = classFile.ConstantPool.ResolveConstant(info.CatchType);
            if (constantInfo is ClassInfo classInfo)
                CatchType = new ClassReference(classFile, classInfo);
        }
        
        /// <summary>
        /// Gets or sets the first instruction of the protected code range.
        /// </summary>
        public ByteCodeInstruction Start
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the final instruction of the protected code range.
        /// </summary>
        public ByteCodeInstruction End
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first instruction of the handler block in the protected code range.
        /// </summary>
        public ByteCodeInstruction HandlerStart
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of exception that is caught (when applicable). 
        /// </summary>
        /// <remarks>
        /// When this property is <c>null</c>, any exception type will be caught. This is also how the finally block
        /// is implemented.
        /// </remarks>
        public ClassReference CatchType
        {
            get;
            set;
        }
    }
}