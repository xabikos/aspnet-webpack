import React, {Component, PropTypes} from 'react';
import {Panel} from 'react-bootstrap';

class RepositoryItem extends Component {
  constructor(props) {
    super(props);

    this.handleClick = this.handleClick.bind(this);
  }

  handleClick() {
    this.props.onSelect(this.props.id);
  }

  render() {
    return (
      <Panel bsStyle="primary" onClick={this.handleClick}>
        <h4 className="card-title">{this.props.full_name}</h4>
        <h6 className="card-subtitle">Score: <strong>{this.props.score}</strong></h6>
        <p className="card-text">{this.props.description}</p>
        <a href={this.props.html_url} target="_blank" className="card-link">Github repo</a>
      </Panel>
    );
  }
}

RepositoryItem.propTypes = {
  id: PropTypes.number.isRequired,
  full_name: PropTypes.string.isRequired,
  description: PropTypes.string,
  html_url: PropTypes.string.isRequired,
  score: PropTypes.number,
  onSelect: PropTypes.func.isRequired,
};

export default RepositoryItem;
