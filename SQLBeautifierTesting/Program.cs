using SQLBeautifier;

namespace SQLBeautifierTesting
{
    public class Program
    {
        public static void Main()
        {
            var sql = "CREATE PROCEDURE GetCustomerOrderSummary\n    @CustomerID INT,\n    @StartDate DATE,\n    @EndDate DATE,\n    @MinTotalAmount DECIMAL(10, 2)\nAS\nBEGIN\n    BEGIN TRANSACTION;\n\n    BEGIN TRY\n        SELECT \n            C.CustomerID,\n            C.Name,\n            C.City,\n            SUM(OD.Quantity) AS TotalQuantity,\n            SUM(OD.Quantity * OD.UnitPrice) AS TotalSpent\n        FROM \n            Customers C\n        INNER JOIN \n            Orders O ON C.CustomerID = O.CustomerID\n        INNER JOIN \n            OrderDetails OD ON O.OrderID = OD.OrderID\n        WHERE \n            C.CustomerID = @CustomerID\n            AND O.OrderDate BETWEEN @StartDate AND @EndDate\n            AND O.TotalAmount >= @MinTotalAmount\n        GROUP BY \n            C.CustomerID, C.Name, C.City\n        ORDER BY \n            TotalSpent DESC;\n\n        COMMIT TRANSACTION;\n    END TRY\n    BEGIN CATCH\n        ROLLBACK TRANSACTION;\n        THROW;\n    END CATCH\nEND\n";
            var factory = new Factory();
            var text = factory.Format(sql);
        }
    }
}
