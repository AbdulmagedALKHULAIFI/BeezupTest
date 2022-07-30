Feature: ConvertCsvFile

Convert Csv File to another types

@ConvertCsvFileToJson
Scenario: Convert Csv file to Json
    Given Csv File
        | sku   | title                   | description                                         | price | stock
        | s1324 | My super product        | My super product it’s the best of the market        | 1.21  | 99
        | x5611 | My second super product | My second super product it’s the best of the market | 7.43  | 53
    And csv delimiter ;
    When Convert file
	Then Verify that the Csv is successfully converted to Json



Scenario: Convert Csv file to Xml
    Given Csv File
        | sku   | title                   | description                                         | price | stock
        | s1324 | My super product        | My super product it’s the best of the market        | 1.21  | 99
        | x5611 | My second super product | My second super product it’s the best of the market | 7.43  | 53
    And csv delimiter ;
    When Convert file
    Then Verify that the Csv is successfully converted to Xml