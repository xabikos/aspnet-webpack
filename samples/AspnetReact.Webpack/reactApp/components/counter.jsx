import React, {Component} from 'react';

class Counter extends Component {
  constructor(props) {
    super(props);
    
    this.state = {
      timeElapsed: 0,
    };
  }
  
  componentDidMount() {
    setInterval(() => {
      this.setState({
        timeElapsed: this.state.timeElapsed + 1
      });
    }, 1000);
  }
  
  
	render() {
    return (
			<div>
		    <span id="seconds">{this.state.timeElapsed} </span> seconds has passed since the component was loaded
			</div>
		);
	}
}

export default Counter;