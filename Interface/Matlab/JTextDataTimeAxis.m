function data = JTextDataTimeAxis( ip, tag, shot, arg1, arg2, arg3, arg4 )
% �ӷ������϶�ȡHDF5��������ʽ�����ݵ�ʱ���ᣬ��JTextData����ʽһģһ��
% ��һ���������Ϊ����������IP����'127.0.0.1:8002'
% �ڶ����������Ϊ��ͨ��Tag���ƣ���'ecei_1'
% �������������Ϊ�ںţ���1063000���������0���ʾ��ǰ�ں�
% û�к�����������ͨ��ȫ������
% ������������Ϊ�Ǹ�����
% ������2������������1Ϊstart������2Ϊlength
% ������3������������1Ϊstart������2Ϊstride������3Ϊcount
% ������4������������1Ϊstart������2Ϊstride������3Ϊcount������4Ϊblock

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