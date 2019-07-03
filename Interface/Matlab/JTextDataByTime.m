function data = JTextDataByTime( ip, tag, shot, arg1, arg2, arg3 )
% �ӷ������϶�ȡHDF5��������ʽ������
% ��һ���������Ϊ����������IP����'127.0.0.1:8002'
% �ڶ����������Ϊ��ͨ��Tag���ƣ���'ecei_1'
% �������������Ϊ�ںţ���1063000���������0���ʾ��ǰ�ں�
% ������3������������1ΪstartTime������2ΪendTime������3Ϊstride

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