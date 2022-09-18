using System;
using System.Xml.Serialization;


namespace BadWitsml
{

    [XmlRoot("logs", Namespace = "http://www.witsml.org/schemas/1series", IsNullable = false)]
    public class logs
    {
        //documentInfo
        [XmlIgnoreAttribute]
        public object documentInfo { get; set; }

        [XmlElement(elementName: "log")]
        public log Log { get; set; }
    }

    //	<log uidWell="W-12" uidWellbore="B-01" uid="f34a"> 
    public class log
    {   
        //documentInfo
        [XmlIgnoreAttribute]
        public object nameWell { get; set; }

        [XmlIgnoreAttribute]
        public object name { get; set; }
        [XmlIgnoreAttribute]
        public object serviceCompany { get; set; }
       [XmlIgnoreAttribute]
        public object runNumber { get; set; }
        
        [XmlArrayItem(ElementName= "data",
                        IsNullable=false,
                        Type = typeof(decimal))]
        [XmlArray("logData")]
        public decimal[] data { get; set; }
    }

    //<logData>
    //<data>87</data>
    //public class logData
    //{
    //
    //}


}
