ALTER TABLE Wiadomosci ADD OperatorId INT NULL 
CONSTRAINT [FK_Wiadomosci_Operatorzy] FOREIGN KEY([OperatorId]) REFERENCES [Business].[Operatorzy] ([ID]);

GO

UPDATE m
SET m.Operator = o.ID
FROM Wiadomosci m
LEFT JOIN Business.Operatorzy o ON o.PracownikID = m.Pracownik
WHERE m.Pracownik IS NOT NULL;

GO

ALTER TABLE Wiadomosci DROP COLUMN Pracownik

GO