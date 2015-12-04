import './style.scss';

import ContentGenerator from './contentGenerator';

const generatedContent = document.createElement('p');
const loremText = ContentGenerator.getContent(15);
generatedContent.innerHTML = loremText;

document.body.appendChild(generatedContent);