namespace ParticeCustomer3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(BanksExcelViewMetaData))]
    public partial class BanksExcelView
    {
    }
    
    public partial class BanksExcelViewMetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 分行代碼 { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        [Required]
        public int 銀行代碼 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 銀行名稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 帳戶名稱 { get; set; }
        
        [StringLength(20, ErrorMessage="欄位長度不得大於 20 個字元")]
        [Required]
        public string 帳戶號碼 { get; set; }
        [Required]
        public bool 已刪除 { get; set; }
    }
}
