function basepath = jtextbasepath( ip )
%JTEXTBASEPATH ��ȡ�ɼ��ļ��ڷ�������ŵĸ�Ŀ¼
%   �������ֻ��һ����Ϊ����������IP

if(nargin ~= 1)
    disp('params error!')
    return;
end
url = strcat('http://',ip,'/dataserver/basepath');
get = webread(url);
json = jsondecode(get);
basepath = json.CFET2CORE_SAMPLE_VAL;  
end

