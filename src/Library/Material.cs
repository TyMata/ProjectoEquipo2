using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase
    /// </summary>
    public class Material
    {
        public string Type{get;set;}
        public string Classification{get;set;}


        public Material (string type ,string classification )
        {
            this.Type=type;
            this.Classification=classification;
        }

    }
}