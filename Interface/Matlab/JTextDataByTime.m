function data = JTextDataByTime( ip, tag, shot, arg1, arg2, arg3 )
% 从服务器上读取HDF5或其它格式的数据
% 第一个输入参数为服务器完整IP，如'127.0.0.1:8002'
% 第二个输入参数为该通道Tag名称，如'ecei_1'
% 第三个输入参数为炮号，如1063000，如果输入0则表示当前炮号
% 后续有3个参数，参数1为startTime，参数2为endTime，参数3为stride

if(nargin ~= 6) 
    disp('params error!')
    return;
end
url = strcat('http://',ip,'/tagserver/databytime/',tag,'/',shot,'/',num2str(arg1),'/',num2str(arg2),'/',num2str(arg3));
option = weboptions('Timeout', 60);
get = webread(url, option);
json = jsondecode(get);
data = json.CFET2CORE_SAMPLE_VAL;  
end