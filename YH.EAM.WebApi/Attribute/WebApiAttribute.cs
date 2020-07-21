using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using YH.EAM.Entity.Tool;
using Victory.Core.Extensions;
using Victory.Core.Enums;

namespace YH.EAM.WebApi.Attribute
{
    public class WebApiAttribute : ActionFilterAttribute
    {



        /// <summary>
        /// Webapi过滤器
        /// </summary>
        /// <param name="Context"></param>
        public override void OnActionExecuting(ActionExecutingContext Context)
        {

            base.OnActionExecuting(Context);

            //配置文件可配置测试环境下，不验证签名
            if (AppConfig.IgnoreApiFilter)
            {
                return;
            }


            var request = Context.HttpContext.Request;
            string timestamp = GetHeaderValue(request, "timestamp");//时间戳
            DateTime time = timestamp.ToLong().ToDateTime(true);   //将时间撮转换成时间 （Todatetime 在long的扩展方法里）

            if (time.AddSeconds(60) < DateTime.Now)
            {
                Context.Result = new JsonResult(new { Success = false, Code = HttpStatusCode.请求超过时间范围.ToInt(), Message = "请求范围不符合要求！" });
                return;
            }

            string key =Entity.Tool.AppConfig.Jwt.ApiKey;//密钥
            string body = GetBodyValueAsync(request);
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append($"path={request.Path}");
            sBuilder.Append($"&timestamp={timestamp}");
            sBuilder.Append($"&body={body}");
            sBuilder.Append($"&key={key}");

            string sign = Victory.Core.Encrypt.Md5.Encrypt32(sBuilder.ToString());
            string signature = GetHeaderValue(request, "SigningKey");//签名串 MD5(path={0}&timestamp={1}&token={2}&body={3}&key={4})

            if (sign != signature)
            {
                Context.Result = new JsonResult(new { Success = false, Code = HttpStatusCode.签名错误.ToInt(), Message = "签名验证不通过！" });
                return;
            }


        }




        /// <summary>
        /// 获取指定key的Header值
        /// </summary>
        /// <param name="httpRequest">HttpRequest对象</param>
        /// <param name="key">要获取Headers的名称</param>
        /// <returns></returns>
        public  string GetHeaderValue( HttpRequest httpRequest, string key)
        {
            return httpRequest.Headers.ContainsKey(key) ? httpRequest.Headers[key].ToString() : string.Empty;
        }

        /// <summary>
        /// 获取Request的Body值
        /// <para>获取Body值后再重新设置Body</para>
        /// </summary>
        /// <param name="httpRequest">HttpRequest对象</param>
        /// <param name="encoding">默认编码为UTF8</param>
        /// <returns></returns>
        public string GetBodyValueAsync(HttpRequest httpRequest, Encoding encoding = null)
        {
            string result = string.Empty;
            try
            {

                using (var mStream = new MemoryStream())
                using (var reader = new StreamReader(mStream))
                {
                    httpRequest.Body.Seek(0, SeekOrigin.Begin);
                    httpRequest.Body.CopyTo(mStream);
                    mStream.Seek(0, SeekOrigin.Begin);
                    result = reader.ReadToEnd();
                    httpRequest.Body.Seek(0, SeekOrigin.Begin);
                }

            }
            catch (Exception e){
                    
            
            }
            return result;
        }

   


    }
}
