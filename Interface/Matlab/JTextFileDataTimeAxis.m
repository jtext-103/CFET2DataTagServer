function data = JTextFileDataTimeAxis( ip, path, arg1, arg2, arg3, arg4 )
% �ӷ������϶�ȡHDF5��������ʽ�����ݵ�ʱ���ᣬ��JTextFileData����ʽһģһ��
% ��һ���������Ϊ����������IP����'127.0.0.1:8002'
% �ڶ����������Ϊͨ������basepath��·������'card0.105xxx.10563xx.1056333.data.0'��ע����'.'�ָ�
% û�к�����������ͨ��ȫ������
% ������������Ϊ�Ǹ�����
% ������2������������1Ϊstart������2Ϊlength
% ������3������������1Ϊstart������2Ϊstride������3Ϊcount
% ������4������������1Ϊstart������2Ϊstride������3Ϊcount������4Ϊblock

if(nargin < 2) 
    disp('params error!')
    return;
end
url = strcat('http://',ip,'/dataserver');
switch(nargin)
    case 2
        url = strcat(url,'/datatimeaxis/',path);
    case 4
        url = strcat(url,'/datatimeaxis/',path,'/',num2str(arg1),'/',num2str(arg2));
    case 5
        url = strcat(url,'/datacomplextimeaxis/',path,'/',num2str(arg1),'/',num2str(arg2),'/',num2str(arg3));
    case 6
        url = strcat(url,'/datacomplextimeaxis/',path,'/',num2str(arg1),'/',num2str(arg2),'/',num2str(arg3),'/',num2str(arg4));
    otherwise
        disp('params error!')
        return
end
option = weboptions('Timeout', 60);
get = webread(url, option);
json = jsondecode(get);
data = json.CFET2CORE_SAMPLE_VAL;  
end

