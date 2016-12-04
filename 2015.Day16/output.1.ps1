$things = "children: 3", "cats: 7","samoyeds: 2","pomeranians: 3","akitas: 0","vizslas: 0",
            "goldfish: 5","trees: 3","cars: 2","perfumes: 1" |
              %{,($_ -split ": ") | %{[pscustomobject]@{Thing=$_[0];Num=$_[1]} } }

cat input.txt -pv i | ?{ !($things | ?{ $i -match $_.Thing -and $i -notmatch "$($_.Thing): $($_.Num)"})}