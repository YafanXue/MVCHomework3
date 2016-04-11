namespace ParticeCustomer3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶分類資訊MetaData))]
    public partial class 客戶分類資訊
    {
    }
    
    public partial class 客戶分類資訊MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(300, ErrorMessage="欄位長度不得大於 300 個字元")]
        public string 分類名稱 { get; set; }
    
        public virtual ICollection<客戶資料> 客戶資料 { get; set; }
    }
}
