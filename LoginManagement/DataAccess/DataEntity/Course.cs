using LoginManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginManagement.DataAccess.DataEntity
{
    public class Course : IEditableObject 
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
            }
        }

        private int _id;
        public int ID
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate == value) return;
                _startDate = value;
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate == value) return;
                _endDate = value;
            }
        }

        #region IEditableObject
        private Course backupCopy;
        private bool inEdit;

        public void BeginEdit()
        {
            if (inEdit)
            {
                return;
            }
            inEdit = true;
            backupCopy = MemberwiseClone() as Course;
        }

        public void CancelEdit()
        {
            if (!inEdit)
            {
                return;
            }
            inEdit = false;
            Name = backupCopy.Name;
            ID = backupCopy.ID;
            StartDate = backupCopy.StartDate;
            EndDate = backupCopy.EndDate;
        }

        public void EndEdit()
        {
            if (!inEdit)
            {
                return;
            }
            inEdit = false;
            backupCopy = null;
        }

        #endregion
    }
}
