using GymManager_BE;

namespace GymManager_BLL;

/// <summary>
/// Interface for payment-related operations in the Gym Manager application.
/// This interface defines methods for managing payments, including retrieving, adding, updating, and deleting payments.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    ///  Retrieves a list of payments within a specified date range for a given user.
    /// This method allows you to filter payments based on a date range and user ID.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="userId"></param>
    /// <returns>A task that represents the asynchronous operation, containing the payments list</returns>
    Task<List<Payment>> GetPayments(DateOnly from, DateOnly to, long userId);

    /// <summary>
    ///  Retrieves a payment by its unique identifier.
    /// This method allows you to fetch a specific payment using its ID.
    /// </summary>
    /// <param name="paymentId"></param>
    /// <returns>A task that represents the asynchronous operation, containing the payment.</returns>
    Task<Payment> GetPaymentById(long paymentId);

    /// <summary>
    /// Adds a new payment to the system.
    /// This method allows you to create a new payment record.
    /// </summary>
    /// <param name="payment">The payment object to be added.</param>
    /// <returns>A task that represents the asynchronous operation, containing the added payment.</returns>
    /// <remarks>This method is used to create a new payment record in the system.</remarks
    Task<Payment> AddPayment(Payment payment);

    /// <summary>
    /// Updates an existing payment in the system.
    /// This method allows you to modify an existing payment record.
    /// </summary>
    /// <param name="paymentId">The unique identifier of the payment to be updated.</param>
    /// <param name="payment">The payment object with updated information.</param>
    /// <returns>A task that represents the asynchronous operation, containing the updated payment.</returns>
    Task<Payment> UpdatePayment(long paymentId, Payment payment);

    /// <summary>
    /// Deletes a payment by its unique identifier.
    /// This method allows you to remove a payment record from the system.
    /// </summary>
    /// <param name="paymentId">The unique identifier of the payment to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation, containing a boolean indicating success or failure.</returns>
    Task<bool> DeletePayment(long paymentId);
}