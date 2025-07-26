using Castle.DynamicProxy;
using Serilog;
using System.Reflection;


namespace SimpleCore.Extensions.Aop
{
    public class ExceptionAop : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
                
                if(invocation.Method.ReturnType == typeof(Task))
                {
                    invocation.ReturnValue = InterceptAsync((Task)invocation.ReturnValue, invocation.Method.Name);
                }
                else if (invocation.Method.ReturnType.IsGenericType &&
                        invocation.Method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    var resultType = invocation.Method.ReturnType.GetGenericArguments()[0];
                    var method = typeof(ExceptionAop).GetMethod(nameof(InterceptAsyncGeneric), BindingFlags.NonPublic | BindingFlags.Instance);
                    var genericMethod = method.MakeGenericMethod(resultType);
                    invocation.ReturnValue = genericMethod.Invoke(this, new object[] { invocation.ReturnValue, invocation.Method.Name });
                }
            }
            catch (Exception ex) 
            {
                Log.Error("ExceptionAop錯誤 value:{ex}", ex);
            }
        }

        private async Task InterceptAsync(Task task, string methodName)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "異步執行發生錯誤，方法名稱: {MethodName}", methodName);
                throw new Exception($"服務執行錯誤，請聯繫管理員。{ex.Message}");
            }
        }

        private async Task<T> InterceptAsyncGeneric<T>(Task<T> task, string methodName)
        {
            try
            {
                return await task;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "異步執行發生錯誤，方法名稱: {MethodName}", methodName);
                throw new Exception($"服務執行錯誤，請聯繫管理員。{ex.Message}");
            }
        }
    }
}
