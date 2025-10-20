using GymManager_BE;

namespace GymManager_BLL;

/// <summary>
/// Interface for fee-related operations in the Gym Manager application.
/// This interface defines methods for managing fees, including retrieving, adding, updating, and deleting fees.
/// </summary>
public interface IFeeService
{
    /// <summary>
    /// Retrieves a list of all the fees.
    /// </summary>
    /// <param name="from">The start date of the range.</param>
    /// <param name="to">The end date of the range.</param>
    /// <param name="userId">The ID of the user for whom fees are being retrieved.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of fees.</returns>
    Task<List<Fee>> GetFees();

    /// <summary>
    /// Retrieves a list of fees within a specified date range for a given user.
    /// </summary>
    /// <param name="from">The start date of the range.</param>
    /// <param name="to">The end date of the range.</param>
    /// <param name="userId">The ID of the user for whom fees are being retrieved.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of fees.</returns>
    Task<List<Fee>> SearchFees(DateOnly? from, DateOnly? to, long? userId);

    /// <summary>
    /// Retrieves a fee by its unique identifier.
    /// </summary>
    /// <param name="feeId">The unique identifier of the fee.</param>
    /// <returns>A task that represents the asynchronous operation, containing the fee if found.</returns>
    Task<Fee> GetFeeById(long feeId);

    /// <summary>
    /// Adds a new fee to the system.
    /// </summary>
    /// <param name="fee">The fee object to be added.</param>
    /// <param name="userId">The ID of the user associated with the fee.</param>
    /// <returns>A task that represents the asynchronous operation, containing the added fee.</returns>
    Task<Fee> AddFee(Fee fee, long userId);

    /// <summary>
    /// Updates an existing fee in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the fee to be updated.</param>
    /// <param name="fee">The fee object with updated information.</param>
    /// <returns>A task that represents the asynchronous operation, containing the updated fee.</returns>
    Task<Fee> UpdateFee(long id, Fee fee);

    /// <summary>
    /// Deletes a fee by its unique identifier.
    /// </summary>
    /// <param name="feeId">The unique identifier of the fee to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation, containing a boolean indicating success or failure.</returns>
    Task<bool> DeleteFee(long feeId);
}