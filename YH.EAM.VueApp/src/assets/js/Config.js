
const Parameters = {

	//�Ƿ��ǿ��������� 1�� ���������� 2�����Ի�����3����ʽ����
	
	Isdevpos: 1,
	ApiKey: "58b59b3ae5d0ec0629950ebdd4dabe39",
	BaseUrl: function () {
		if (Parameters.Isdevpos == 1) {
			return "http://www.asm.cn:50428";
		}
		if (Parameters.Isdevpos == 2) {
			return "https://asm.yhwins.com:52421";
		}
		if (Parameters.Isdevpos == 3) {
			return "https://asm.yhwins.com:52420";
		}

	}

}


export default {
	Parameters
}