using Microsoft.AspNetCore.Http;
using ShareFile.Config;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ShareFile.Utils
{
    public class IpUtil
    {
        public static string GetUserIP(HttpRequest request)
        {
            string ip = request.Headers["x-forwarded-for"];
            if (string.IsNullOrWhiteSpace(ip) || ip.Length == 0 || "unknown" == ip || AuthFilter.WHITE_LIST.Contains(ip))
            {
                ip = request.Headers["Proxy-Client-IP"];
            }
            if (string.IsNullOrWhiteSpace(ip) || ip.Length == 0 || "unknown" == ip || AuthFilter.WHITE_LIST.Contains(ip))
            {
                ip = request.Headers["WL-Proxy-Client-IP"];
            }
            if (string.IsNullOrWhiteSpace(ip) || ip.Length == 0 || "unknown" == ip || AuthFilter.WHITE_LIST.Contains(ip))
            {
                ip = request.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }


        public static string GetLoacalIPMaybeVirtualNetwork()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "";
        }

        public static string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                // 网络类型是所规定的并且网络再运行状态
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }

        public static string IPV4()
        {
            string ipv4 = GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            // 如果不是无线网卡，则获取有线网卡的地址
            if (ipv4 == "")
            {
                ipv4 = GetLocalIPv4(NetworkInterfaceType.Ethernet);
                // 如果有线网卡也没有获取到数据，则使用最开始可能包含虚拟网卡的方法来获取IP
                if (ipv4 == "")
                {
                    ipv4 = GetLoacalIPMaybeVirtualNetwork();
                }
            }
            return ipv4;
        }
    }
}
