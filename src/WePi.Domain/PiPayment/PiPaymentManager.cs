using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace WePi.Domain;

public class PiPaymentManager: DomainService
{
    protected readonly IRepository<PiPayment, Guid> _repository;
    public PiPaymentManager(IRepository<PiPayment, Guid> repository)
    {
        //IRepository<PiPayment, Guid> repository
        _repository = repository;
    }

    public async Task<PiPayment> GetByIdOrIdentifierAsync(string identifier, Guid? id)
    {
        //Obtain the IQueryable<Person>
        IQueryable<PiPayment> queryable = await _repository.GetQueryableAsync();

        //Create a query
        IQueryable<PiPayment> query;
        if (id != null)
            query = queryable.Where(x => x.Id == id);
        else
            query = queryable.Where(x => x.Identifier == identifier);

        //Execute the query to get list of people
        var payment = query.FirstOrDefault();

        return payment;
    }

    public async Task<PiPayment> UpdateTransaction(PiPayment payment, string txid)
    {
        payment.TxId = txid;
        return await _repository.UpdateAsync(payment, true);
    }

    public async Task<PiPayment> UpdateTransaction(PiPayment payment)
    {
        return await _repository.UpdateAsync(payment, true);
    }

    public async Task<List<PiPayment>> GetCreatedPaymentsAsync()
    {
        //Obtain the IQueryable<PiPayment>
        IQueryable<PiPayment> queryable = await _repository.GetQueryableAsync();

        //Create a query
        var query = queryable.Where(x=>x.Finished == false && x.A2U == 1 && !string.IsNullOrEmpty(x.Identifier));

        //Execute the query to get list of people
        var payments = query.ToList();

        return payments;
    }

    public async Task<List<PiPayment>> GetNewPaymentsAsync()
    {
        //Obtain the IQueryable<Person>
        IQueryable<PiPayment> queryable = await _repository.GetQueryableAsync();

        //Create a query
        var query = queryable.Where(x => x.Finished == false && x.A2U == 1 && string.IsNullOrEmpty(x.Identifier));

        //Execute the query to get list of people
        var payments = query.ToList();

        return payments;
    }
}
