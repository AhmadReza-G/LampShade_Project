using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application;
public class OperationResult
{
    private bool IsSucceded { get; set; } = false;
    private string Message { get; set; } = string.Empty;
    public OperationResult Succeded(string message = "عملیات با موفقیت انجام شد.")
    {
        IsSucceded = true;
        Message = message;
        return this;
    }
    public OperationResult Failed(string message)
    {
        IsSucceded = false;
        Message = message;
        return this;
    }
}
