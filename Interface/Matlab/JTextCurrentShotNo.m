function currentShotNo = JTextCurrentShotNo( ip )
% 获取最新炮号
% 输入参数只有一个，为服务器完整IP

if(nargin ~= 1)
    disp('params error!')
    return;
end
url = strcat('http://',ip,'/tag/currentshotno');
get = webread(url);
json = jsondecode(get);
currentShotNo = json.CFET2CORE_SAMPLE_VAL;  
end