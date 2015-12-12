import React, {Component} from 'react';

import '../styles/site.scss';

import Counter from './counter.jsx';


class App extends Component {
  render() {
    return (
	  <div>
	    <Counter />
	  </div>
    );
  }
}

export default App;