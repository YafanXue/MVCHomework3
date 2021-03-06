﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ParticeCustomer3.Models
{
    public class UpdateBatchContact 
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [MobileFormatCheck(ErrorMessage ="手機格式錯誤")]
        public string 手機 { get; set; }
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
        public int 客戶Id { get; set; }

        
    }
}