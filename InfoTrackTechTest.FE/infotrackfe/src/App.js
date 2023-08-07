import React, { useState } from 'react';
import Navbar from './shared/navbar/Navbar';
import CountAnimation from './CountAnimation';

function App() {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchUrl, setSearchUrl] = useState('');
  const [resultData, setResultData] = useState(null);
  const [error, setError] = useState('');

  var dateToday = resultData && new Date(resultData.dateToday).toLocaleDateString()

  const handleSubmit = async (e) => {
    e.preventDefault();
  
    try {
      const response = await fetch('https://localhost:7295/api/scrapeddata', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ searchTerm, searchUrl }),
      });
  
      if (response.ok) {
        setError('');
        const data = await response.json();
        setResultData(data);
      } else {
        const errorResponse = await response.json();
        setError('*Please make sure to fill all fields and then submit');
      }
    } catch (error) {
      console.error("An error occurred:", error);
      setError('An error occurred while processing your request.');
    }
  };

  return (
    <>
      <Navbar />
      <div className="main-section">
        <div className="main-container">
        {error && <div className="error-message">{error}</div>}

          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <label htmlFor="searchTerm">Search Term</label>
              <input
                type="text"
                className="form-control"
                id="searchTerm"
                placeholder="Enter Search Term"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
              />
            </div>
            <div className="form-group">
              <label htmlFor="searchUrl">Search URL</label>
              <input
                type="text"
                className="form-control"
                id="searchUrl"
                placeholder="Enter Search URL"
                value={searchUrl}
                onChange={(e) => setSearchUrl(e.target.value)}
              />
            </div>
            <button type="submit" className="btn results-btn">
              Submit
            </button>
          </form>
        </div>
      </div>

      {resultData ? (
        <div className="main-section-results">
          <div className="main-container-results">
          <h4>Todays Daily Dashboard Results for <em>{dateToday}</em></h4>

            <div className="row">
              <div className="col">
                <p><b>The count today is (excluding any links): </b></p>
                <p className="result-number"><CountAnimation end={resultData.todaysCountExcludingLinks} /></p><b>/ 100 results</b>
              </div>
              <div className="col">
                <p><b>The count today is (including any links): </b></p>
                <p className="result-number"><CountAnimation end={resultData.todaysCountIncludingLinks} /></p><b>/ 100 results</b>
              </div>
              <div className="col">
                <p><b>Weekly total:</b></p>
                <p className="result-number">{resultData.todaysCountIncludingLinks}</p>
              </div>
            </div>
            <div className="summary">
              <h6>Above is for Search Url: {searchUrl}</h6>
              <h6>Above is for Search Term: {searchTerm}</h6>
            </div>
          </div>
        </div>
      ) : (
        <div className="main-section-results">
          <div className="main-container-results">
            <h4>Todays Daily Dashboard Results</h4>
            <h6>Please Type in Search Term and Url and click submit to review results for today.. </h6>
          </div>
        </div>
      )}
    </>
  );
}

export default App;
