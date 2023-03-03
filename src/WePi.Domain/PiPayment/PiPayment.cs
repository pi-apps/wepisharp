using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities;

namespace WePi.Domain;

[Table("pipayment")]
public class PiPayment : IEntity<Guid>
{
    //public PiPayment()
    //{
    //}
    //public PiPayment(Guid id)
    //{
    //    Id = id;
    //}
    //[Column("id")]
    //public override Guid Id { get; }

    [Column("id")]
    public Guid Id { get; set; }

    public object[] GetKeys()
    {
        return new object[] { Id };
    }
    
    [Column("a2u")]
    public int A2U { get; set; }
    
    //[Column("asset")]
    //public string Asset { get; set; }
    
    //[Column("fee")]
    //public double Fee { get; set; }
    
    [Column("step")]
    public int Step { get; set; }
    
    [Column("finished")]
    public bool Finished { get; set; }
    
    [Column("published")]
    public DateTime Published { get; set; }
    
    [Column("updated")]
    public DateTime? Updated { get; set; }

    // PaymentDTO fields from here
    [Column("identifier")]
    public string Identifier { get; set; }
    
    [Column("user_uid")]
    public string UserUid { get; set; }
    
    [Column("amount")]
    public double Amount { get; set; }
    
    [Column("memo")]
    public string Memo { get; set; }
    
    [Column("from_address")]
    public string FromAddress { get; set; }
    
    [Column("to_address")]
    public string ToAddress { get; set; }
    
    [Column("direction")]
    public string Direction { get; set; }
    
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
    
    [Column("network")]
    public string Network { get; set; }
    
    [Column("approved")]
    public bool Approved { get; set; }
    
    [Column("verified")]
    public bool Verified { get; set; }
    
    [Column("completed")]
    public bool Completed { get; set; }
    
    [Column("cancelled")]
    public bool Cancelled { get; set; }
    
    [Column("user_cancelled")]
    public bool UserCancelled { get; set; }
    
    [Column("tx_verified")]
    public bool TxVerified { get; set; }
    
    [Column("tx_id")]
    public string TxId { get; set; }
    
    [Column("tx_link")]
    public string TxLink { get; set; }
    //[Column(type= "jsonb ")]
    //public object metadata { get; set; }
}
