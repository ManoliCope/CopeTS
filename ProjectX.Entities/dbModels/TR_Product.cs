using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class TR_Product
    {
        public int PR_Id { get; set; }
        public string PR_Title { get; set; }
        public string PR_Description { get; set; }
        public bool PR_Is_Family { get; set; }
        public DateTime PR_Activation_Date { get; set; }
        public bool PR_Is_Active { get; set; }
        public double PR_Is_Deductible { get; set; }
        public double PR_Sports_Activities { get; set; }
        public double PR_Additional_Benefits { get; set; }
        public bool PR_Is_Individual { get; set; }
        public bool PR_Is_Group { get; set; }
        public int PR_Deductible_Format { get; set; }
        public int PR_Sports_Activity_Format { get; set; }
        public int PR_Additional_Benefits_Format { get; set; }
    }
}
