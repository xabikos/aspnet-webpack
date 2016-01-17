import React, {Component} from 'react';

import '../styles/site.scss';

import Counter from './counter.jsx';
import smallImage from '../images/react-small.png';
import largeImage from '../images/react-large.jpg';

class App extends Component {
  render() {
    return (
	  <div>
	    <Counter />
      <div id="small-image">
        <p>
          This is an example of an image which is embeded as bese 64 encoded using the url loader
        </p>
        <img alt="small-image" src={smallImage} />
      </div>
      <div id="large-image">
        <p>
          This is an example of a <strong>large</strong> image which is too large to base64 encoded and is just copied to output using the file loader
        </p>
        <img alt="large-image" src={largeImage} />
      </div>
	  </div>
    );
  }
}

export default App;
