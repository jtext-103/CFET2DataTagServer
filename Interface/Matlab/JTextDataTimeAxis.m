function data = JTextDataTimeAxis( ip, tag, shot, arg1, arg2, arg3, arg4 )
% 从服务器上读取HDF5或其它格式的数据的时间轴，和JTextData的形式一模一样
% 第一个输入参数为服务器完整IP，如'127.0.0.1:8002'
% 第二个输入参数为该通道Tag名称，如'ecei_1'
% 第三个输入参数为炮号，如1063000，如果输入0则表示当前炮号
% 没有后续参数，该通道全部读出
% 后续参数类型为非负整数
% 后续有2个参数，参数1为start，参数2为length
% 后续有3个参数，参数1为start，参数2为stride，参数3为count
% 后续有4个参数，参数1为start，参数2为stride，参数3为count，参数4为block

if(nargin < 3) 
    disp('params error!')
    return;
end
url = strcat('http://',ip,'/tagserver');
switch(nargin)
    case 3
        url = strcat(url,'/datatimeaxis/',tag,'/',shot);
    case 5
        url = strcat(url,'/datatimeaxis/',tag,'/',shot,'/',num2str(arg1),'/',num2str(arg2));
    case 6
        url = strcat(url,'/datacomplextimeaxis/',tag,'/',shot,'/',num2str(arg1),'/',num2str(arg2),'/',num2str(arg3));
    case 7
        url = strcat(url,'/datacomplextimeaxis/',tag,'/',shot,'/',num2str(arg1),'/',num2str(arg2),'/',num2str(arg3),'/',num2str(arg4));
    otherwise
        disp('params error!')
        return
end
option = weboptions('Timeout', 60);
get = webread(url, option);
json = jsondecode(get);
data = json.CFET2CORE_SAMPLE_VAL;  
end