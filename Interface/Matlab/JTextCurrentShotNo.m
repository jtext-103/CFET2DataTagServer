function currentShotNo = JTextCurrentShotNo( ip )
% ��ȡ�����ں�
% �������ֻ��һ����Ϊ����������IP

if(nargin ~= 1)
    disp('params error!')
    return;
end
url = strcat('http://',ip,'/tag/currentshotno');
get = webread(url);
json = jsondecode(get);
currentShotNo = json.CFET2CORE_SAMPLE_VAL;  
end