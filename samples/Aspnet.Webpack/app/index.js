import './styles/general.css';
import './styles/style.scss';
import './styles/lessStyle.less';

import ContentGenerator from './contentGenerator';

const generatedContent = document.createElement('p');
const loremText = ContentGenerator.getContent(45);
generatedContent.innerHTML = loremText;

document.body.appendChild(generatedContent);