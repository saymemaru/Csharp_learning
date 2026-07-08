using PanelTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelTest.ViewModels
{
    public class PersonItemViewModel : ValidationViewModelBase
    {
        readonly Person _model;
        public PersonItemViewModel(Person model)
        {
           _model = model;
        }

        public int Id => _model.Id;

        public string? FirstName
        {
            get { return _model.FirstName; }
            set 
            {
                _model.FirstName = value; 
                RaiseProertyChanged();

                //数据验证
                if(string.IsNullOrEmpty(_model.FirstName))
                    AddError("FirstName is required");
                else
                    ClearError();
            }
        }

        public string? LastName
        {
            get { return _model.LastName; }
            set
            {
                _model.LastName = value;
                RaiseProertyChanged();
            }
        }

        public bool IsDeveloper
        {
            get { return _model.IsDeveloper; }
            set
            {
                _model.IsDeveloper = value;
                RaiseProertyChanged();
            }
        }
    }
}
