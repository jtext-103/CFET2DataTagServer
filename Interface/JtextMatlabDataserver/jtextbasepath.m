function basepath = jtextbasepath( ip )
%JTEXTBASEPATH 获取采集文件在服务器存放的根目录
%   输入参数只有一个，为服务器完整IP

if(nargin ~= 1)
    disp('params error!')
    return;
end
url = strcat('http://',ip,'/dataserver/basepath');
get = webread(url);
json = jsondecode(get);
basepath = json.CFET2CORE_SAMPLE_VAL;  
end

