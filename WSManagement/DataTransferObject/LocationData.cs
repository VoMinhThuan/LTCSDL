﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTransferObject
{
    public class LocationData
    {
         public int RowID { get; set; }
        public string No_ { get; set; }
        public string Name{ get; set; }
         
        public LocationData(int _rowID,string _no_, string _name)
        {
            this.RowID = _rowID;
            this.No_= _no_;
            this.Name= _name;            
        }
        public LocationData()
        {
        }
    }
}
