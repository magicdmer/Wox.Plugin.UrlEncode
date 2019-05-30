using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Wox.Plugin.UrlEncode
{
    public class Main : IPlugin
    {
        

        public List<Result> Query(Query query)
        {
            var list = new List<Result>();
            if (query.ActionParameters.Count == 0)
            {
                list.Add(new Result() {
                  IcoPath ="Images\\app.png",
                   Title = "Please input a string"
                });
                return list;
            }

            if (query.ActionParameters.Count == 1)
            {
                var str = query.ActionParameters[0];
                if (str == "-g" || str == "-u")
                {
                    list.Add(new Result()
                    {
                        IcoPath = "Images\\app.png",
                        Title = "Please input a string"
                    });
                    return list;
                }

                list.Add(new Result()
                {
                    IcoPath = "Images\\encode.png",
                    Title = HttpUtility.UrlEncode(str),
                    SubTitle = "Copy to clipboard",
                    Action = (c) =>
                    {
                        Clipboard.SetText(HttpUtility.UrlEncode(str));
                        return true;
                    }
                });
                list.Add(new Result()
                {
                    IcoPath = "Images\\decode.png",
                    Title = HttpUtility.UrlDecode(str),
                    SubTitle = "Copy to clipboard",
                    Action = (c) =>
                    {
                        Clipboard.SetText(HttpUtility.UrlDecode(str));
                        return true;
                    }
                });

                if (str.Contains("%"))
                {
                    // may be a encode string, change order
                    list.Reverse();
                }
            }
            else if (query.ActionParameters.Count == 2)
            {
                var StrEncode = query.ActionParameters[0];
                var str = query.ActionParameters[1];
                var EncodeString = "";
                var DecodeString = "";

                if (StrEncode == "-g")
                {
                    byte[] data = Encoding.GetEncoding("GBK").GetBytes(str);
                    EncodeString = HttpUtility.UrlEncode(data);
                    DecodeString = HttpUtility.UrlDecode(data, Encoding.GetEncoding("GBK"));
                }
                else if (StrEncode == "-u")
                {
                    EncodeString = HttpUtility.UrlEncode(str);
                    DecodeString = HttpUtility.UrlDecode(str);
                }

                list.Add(new Result()
                {
                    IcoPath = "Images\\encode.png",
                    Title = EncodeString,
                    SubTitle = "Copy to clipboard",
                    Action = (c) =>
                    {
                        Clipboard.SetText(EncodeString);
                        return true;
                    }
                });
                list.Add(new Result()
                {
                    IcoPath = "Images\\decode.png",
                    Title = DecodeString,
                    SubTitle = "Copy to clipboard",
                    Action = (c) =>
                    {
                        Clipboard.SetText(DecodeString);
                        return true;
                    }
                });

                if (str.Contains("%"))
                {
                    // may be a encode string, change order
                    list.Reverse();
                }
            }

            return list;
        }

        public void Init(PluginInitContext context)
        {

        }
    }
}
