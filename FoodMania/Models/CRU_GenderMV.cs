﻿using Dblayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodMania.Models
{
    public class CRU_GenderMV
    {
        // Call this Consturctor To Add New Entry 
        public CRU_GenderMV()
        {
            GetAllGenders();
        }
        // Call this Consturctor To Edit Entry 
        public CRU_GenderMV(int? id)
        {
            GetAllGenders();
            var editgender = new FoodManiaEntities().GenderTables.Where(g => g.GenderID == id).FirstOrDefault();
            if (editgender != null)
            {
                GenderID = editgender.GenderID;
                GenderTitle = editgender.GenderTittle;
            }
            else
            {
                GenderID = 0;
                GenderTitle = string.Empty;
            }
        }
        public int GenderID { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Enter Gender")]
        public string GenderTitle { get; set; }
        public List<GenderMV> List_Genders { get; set; }

        private void GetAllGenders()
        {
            List_Genders = new List<GenderMV>();
            foreach (var gender in new FoodManiaEntities().GenderTables.ToList())
            {
                List_Genders.Add(new GenderMV()
                {
                    GenderID = gender.GenderID,
                    GenderTittle = gender.GenderTittle
                });
            }
        }

    }
}