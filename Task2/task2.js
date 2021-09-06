import { SHA3 } from 'sha3';
import fs from 'fs';

const hash = new SHA3(256);

function getFiles(path, callback) {
    fs.readdir(path, (err, files) => {
        files.forEach(file => {
            let stat = fs.statSync(file);
            if (stat.isFile())
            fs.readFile(file, 'utf8', function (err, data) {
                if (err) return console.log(err);
                callback(file, data);
            });
        });
    });
}

getFiles(process.cwd(), (fileName, fileData) => {
    hash.reset();
    hash.update(fileData);
    console.log(`${fileName} ${hash.digest('hex')}`);
});
