import React, {Component, PropTypes} from 'react';
import {Input, Button} from 'react-bootstrap';
import {AutoAffix} from 'react-overlays';

class SearchForm extends Component {
  constructor(props) {
    super(props);

    this.handleClick = this.handleClick.bind(this);
  }

  handleClick() {
    const repositoryValue = this.refs.repository.getValue();
    const languageValue = this.refs.language.getValue();
    if (repositoryValue) {
      if (languageValue !== 'undefined') {
        this.props.search(repositoryValue, languageValue);
      } else {
        this.props.search(repositoryValue);
      }
    }
  }

  render() {
    return (
      <AutoAffix viewportOffsetTop={55}>
        <div>
          <Input
            type="text"
            ref="repository"
            label="Enter a keyword to search Github"
            placeholder="search term"
          />
          <Input type="select" ref="language" label="Optianally select a language">
            <option value="undefined">Select a language</option>
            <option value="javascript">JavaScript</option>
            <option value="csharp">C#</option>
            <option value="python">Python</option>
            <option value="ruby">Ruby</option>
            <option value="java">Java</option>
          </Input>
          <Button bsStyle="primary" onClick={this.handleClick}>Search</Button>
        </div>
      </AutoAffix>
    );
  }
}

SearchForm.propTypes = {
  search: PropTypes.func.isRequired,
};

export default SearchForm;
