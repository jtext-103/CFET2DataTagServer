function data = jtextdata( ip, path, arg1, arg2, arg3, arg4 )
%JTEXTDATA 从服务器上读取HDF5或其它格式的数据
%   第一个输入参数为服务器完整IP，如'127.0.0.1:8002'
%   第二个输入参数为通道基于basepath的路径，如'1056333.data.0'，注意用'.'分隔
%   第二个输入参数也可以是tag名称，如'ecei_001'
%   没有后续参数，该通道全部读出
%   后续参数类型为非负整数
%   后续有2个参数，参数1为start，参数2为length
%   后续有3个参数，参数1为start，参数2为stride，参数3为count
%   后续有4个参数，参数1为start，参数2为stride，参数3为count，参数4为block

if(nargin < 2) 
    disp('params error!')
    return;
end
url = strcat('http://',ip,'/dataserver');
switch(nargin)
    case 2
        url = strcat(url,'/data/',path);
    case 4
        url = strcat(url,'/data/',path,'/',num2str(arg1),'/',num2str(arg2));
    case 5
        url = strcat(url,'/datacomplex/',path,'/',num2str(arg1),'/',num2str(arg2),'/',num2str(arg3));
    case 6
        url = strcat(url,'/datacomplex/',path,'/',num2str(arg1),'/',num2str(arg2),'/',num2str(arg3),'/',num2str(arg4));
    otherwise
        disp('params error!')
        return
end
option = weboptions('Timeout', 60);
get = webread(url, option);
json = jsondecode(get);
data = json.CFET2CORE_SAMPLE_VAL;  
end

