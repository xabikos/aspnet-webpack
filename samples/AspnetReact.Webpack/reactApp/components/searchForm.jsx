import React, {Component, PropTypes} from 'react';
import {FormGroup, ControlLabel, FormControl, Button} from 'react-bootstrap';
import {AutoAffix} from 'react-overlays';

class SearchForm extends Component {
  constructor(props) {
    super(props);

    this.state = {
      repository: '',
      language: '',
    };
    this.handleRepositoryChange = this.handleRepositoryChange.bind(this);
    this.handleLanguageChange = this.handleLanguageChange.bind(this);
    this.handleSubmitClick = this.handleSubmitClick.bind(this);
  }

  handleRepositoryChange(e) {
    this.setState({repository: e.target.value});
  }

  handleLanguageChange(e) {
    this.setState({language: e.target.value});
  }

  handleSubmitClick() {
    const {repository, language} = this.state;
    if (repository) {
      if (language) {
        this.props.search(repository, language);
      } else {
        this.props.search(repository);
      }
    }
  }

  render() {
    return (
      <AutoAffix viewportOffsetTop={55}>
        <div>
          <FormGroup controlId="repositorySearch">
            <ControlLabel>Enter a keyword to search Github</ControlLabel>
            <FormControl
              type="text"
              value={this.state.repository}
              onChange={this.handleRepositoryChange}
              placeholder="search term"
            />
          </FormGroup>
          <FormGroup controlId="formControlsSelect">
            <ControlLabel>Optianally select a programming language</ControlLabel>
            <FormControl
              componentClass="select"
              onChange={this.handleLanguageChange}
              placeholder="Select a programming language">
              <option value="undefined">Select a language</option>
              <option value="javascript">JavaScript</option>
              <option value="csharp">C#</option>
              <option value="python">Python</option>
              <option value="ruby">Ruby</option>
              <option value="java">Java</option>
            </FormControl>
          </FormGroup>
          <Button bsStyle="primary" onClick={this.handleSubmitClick}>Search</Button>
        </div>
      </AutoAffix>
    );
  }
}

SearchForm.propTypes = {
  search: PropTypes.func.isRequired,
};

export default SearchForm;
