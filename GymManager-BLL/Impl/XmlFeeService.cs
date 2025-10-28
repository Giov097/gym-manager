using GymManager_BE;
using GymManager_BLL.Exceptions;
using GymManager_MPP;

namespace GymManager_BLL.Impl;

public class XmlFeeService : IFeeService
{
    private readonly XmlFeeMapper _mapper = new("../../../data.xml");

    public Task<List<Fee>> GetFees()
    {
        return _mapper.GetAll();
    }

    public Task<List<Fee>> SearchFees(DateOnly? from, DateOnly? to, long? userId)
    {
        return _mapper.GetAll();
    }

    public Task<Fee> GetFeeById(long feeId)
    {
        return _mapper.GetById(feeId)
            .ContinueWith(fee => fee.Result ?? throw new FeeNotFoundException());
    }

    public Task<Fee> AddFee(Fee fee, long userId)
    {
        return _mapper.Create(fee, userId);
    }

    public Task<Fee> UpdateFee(long id, Fee fee)
    {
        return _mapper.GetById(id).ContinueWith(existingFee =>
        {
            if (existingFee.Result == null)
            {
                throw new FeeNotFoundException();
            }

            existingFee.Result.Amount = fee.Amount;
            existingFee.Result.StartDate = fee.StartDate;
            existingFee.Result.EndDate = fee.EndDate;
            return _mapper.Update(existingFee.Result)
                .ContinueWith(success =>
                    success.Result
                        ? existingFee.Result
                        : throw new ProcessingException("No se pudo actualizar la cuota")).Result;
        });
    }

    public Task<bool> DeleteFee(long feeId)
    {
        return _mapper.GetById(feeId).ContinueWith(fee =>
            fee.Result == null
                ? throw new FeeNotFoundException()
                : _mapper.Delete(feeId).Result);
    }
}