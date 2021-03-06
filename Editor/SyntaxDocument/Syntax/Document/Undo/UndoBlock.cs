
namespace AIMS.Libraries.CodeEditor.Syntax
{
    //what kind of undoaction is it?
    /// <summary>
    /// 
    /// </summary>
    public enum UndoAction
    {
        /// <summary>
        /// 
        /// </summary>
        InsertRange = 1,
        /// <summary>
        /// 
        /// </summary>
        DeleteRange = 2,
        /// <summary>
        /// 
        /// </summary>
        ReplaceRange = 3,
    }

    //object that holds undo information
    /// <summary>
    /// 
    /// </summary>
    public sealed class UndoBlock
    {
        /// <summary>
        /// 
        /// </summary>
        public string Text = "";
        /// <summary>
        /// 
        /// </summary>
        public string ReplacedText = "";


        /// <summary>
        /// 
        /// </summary>
        public TextPoint Position = new TextPoint(0, 0);

        /// <summary>
        /// 
        /// </summary>
        public UndoAction Action = 0;
    }
}