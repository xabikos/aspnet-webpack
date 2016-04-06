import './styles/general.css';
import './styles/style.scss';
import './styles/lessStyle.less';

import ContentGenerator from './contentGenerator';

const hrContent = document.createElement('hr');
document.body.appendChild(hrContent);
document.body.appendChild(hrContent);
document.body.appendChild(hrContent);

const generatedContent = document.createElement('p');
const loremText = ContentGenerator.getContent(5);
generatedContent.innerHTML = loremText;

document.body.appendChild(generatedContent);