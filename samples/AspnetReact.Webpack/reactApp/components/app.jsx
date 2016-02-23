import React, {Component} from 'react';

import '../styles/site.scss';

import Counter from './counter.jsx';
import MainController from './mainController.jsx';
import githubLogo from '../images/github-logo.png';
import largeImage from '../images/react-large.png';

class App extends Component {
  render() {
    return (
      <div>
        <MainController />
        <img id="background-image" alt="large-image" src={largeImage} />
        <img id="github-logo" alt="github logo" src={githubLogo} />
      </div>
    );
  }
}

export default App;
