

import md5 from 'js-md5/src/md5'

const Singin = function (path, jsonString, ApiKey, timestamp) {

	let enString = "";
	enString += "path=" + path;
	enString += "&timestamp=" + timestamp;
	enString += "&body=" + jsonString;
	enString += "&key=" + ApiKey;

	return md5.hex(enString).toUpperCase();

}


export default {
	Singin
}


