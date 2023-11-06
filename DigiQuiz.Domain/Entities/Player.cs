﻿namespace DigiQuiz.Domain.Entities;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
    public DateTime GameDate { get; set; }
}